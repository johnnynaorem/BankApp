package main

import (
	"bytes"
	"encoding/json"
	"myModule/models"
	"net/http"
	"net/http/httptest"
	"strconv"
	"testing"

	"github.com/gin-gonic/gin"
	"github.com/stretchr/testify/assert"
	"github.com/stretchr/testify/mock"
	"go.uber.org/zap"
	"gorm.io/driver/mysql"
	"gorm.io/gorm"
)

// ? MockDB is a mock implementation of the GORM DB interface
type MockDB struct {
	mock.Mock
}

func (m *MockDB) Where(query interface{}, args ...interface{}) *gorm.DB {
	// ? Return self to chain methods
	return &gorm.DB{Error: gorm.ErrRecordNotFound}
}

func (m *MockDB) First(dest interface{}, conds ...interface{}) *gorm.DB {
	args := m.Called(dest, conds)
	return args.Get(0).(*gorm.DB)
}

func (m *MockDB) Create(value interface{}) *gorm.DB {
	args := m.Called(value)
	return args.Get(0).(*gorm.DB)
}

// ? setupTest initializes test environment
func setupTest() (*gin.Engine, *gorm.DB) {
	gin.SetMode(gin.TestMode)
	router := gin.New()

	// ? Initialize a mock DB
	db, err := gorm.Open(mysql.Open("root:Johnny02@tcp(127.0.0.1:3306)/bankappdb?charset=utf8mb4&parseTime=True&loc=Local"), &gorm.Config{})
	if err != nil {
		panic("Failed to create mock database")
	}

	// ? Set global variables
	bankAppDbConnector = db

	// ? Initialize logger if not already initialized
	if logger == nil {
		logger, _ = zap.NewDevelopment()
	}

	return router, db
}

func TestAddCustomer(t *testing.T) {
	t.Run("Successful Customer Creation", func(t *testing.T) {
		router, db := setupTest()

		// ? Setup test data
		customer := models.Customers{
			FirstName:   "John",
			LastName:    "Doe",
			Email:       "joh@example.com",
			Address:     "123 Main St",
			City:        "New York",
			Phone:       "1234560890",
			DateOfBirth: "2002-02-28",
		}

		// ? Create request
		jsonValue, _ := json.Marshal(customer)
		req, _ := http.NewRequest("POST", "/add-customer", bytes.NewBuffer(jsonValue))
		req.Header.Set("Content-Type", "application/json")

		// ? Setup response recorder
		w := httptest.NewRecorder()

		// ? Setup route
		router.POST("/add-customer", AddCustomer)
		router.ServeHTTP(w, req)

		// ? Assert response
		assert.Equal(t, http.StatusCreated, w.Code)

		var response map[string]string
		err := json.Unmarshal(w.Body.Bytes(), &response)
		assert.Nil(t, err)
		assert.Equal(t, "Customer Created Successfully!", response["message"])

		// ? Verify customer was created in database
		var createdCustomer models.Customers
		result := db.Where("email = ?", customer.Email).First(&createdCustomer)
		assert.Nil(t, result.Error)
		assert.Equal(t, customer.FirstName, createdCustomer.FirstName)
	})

	t.Run("Duplicate Customer", func(t *testing.T) {
		router, db := setupTest()

		// ? Create initial customer
		customer := models.Customers{
			FirstName:   "John",
			LastName:    "Doe",
			Email:       "john@example.com",
			Address:     "123 Main St",
			City:        "New York",
			Phone:       "12345689",
			DateOfBirth: "2002-02-28",
		}
		db.Create(&customer)

		// ? Try to create duplicate customer
		jsonValue, _ := json.Marshal(customer)
		req, _ := http.NewRequest("POST", "/add-customer", bytes.NewBuffer(jsonValue))
		req.Header.Set("Content-Type", "application/json")

		w := httptest.NewRecorder()
		router.POST("/add-customer", AddCustomer)
		router.ServeHTTP(w, req)

		assert.Equal(t, http.StatusConflict, w.Code)

		var response map[string]string
		err := json.Unmarshal(w.Body.Bytes(), &response)
		assert.Nil(t, err)
		assert.Equal(t, "Customer Already Exist", response["message"])
	})

	t.Run("Invalid Request Payload", func(t *testing.T) {
		router, _ := setupTest()

		// ? Send invalid JSON
		req, _ := http.NewRequest("POST", "/add-customer", bytes.NewBuffer([]byte(`{"invalid json`)))
		req.Header.Set("Content-Type", "application/json")

		w := httptest.NewRecorder()
		router.POST("/add-customer", AddCustomer)
		router.ServeHTTP(w, req)

		assert.Equal(t, http.StatusBadRequest, w.Code)
	})
}

func TestGenerateAccountNumber(t *testing.T) {
	// ? Test account number generation
	accountNumber := generateAccountNumber()

	// ? Check length
	assert.Equal(t, 14, len(accountNumber))

	// ? Check if it's a valid number
	num, err := strconv.ParseInt(accountNumber, 10, 64)
	assert.Nil(t, err)
	assert.GreaterOrEqual(t, num, int64(10000000000000))
	assert.LessOrEqual(t, num, int64(90000000000000))

	// ? Generate another number and verify it's different (random)
	// accountNumber2 := generateAccountNumber()
	// assert.NotEqual(t, accountNumber, accountNumber2)
}
