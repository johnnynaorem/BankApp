package main

import (
	"fmt"
	"math/rand"
	"myModule/models"
	"net/http"
	"strconv"
	"time"

	"github.com/gin-gonic/gin"
	"go.uber.org/zap"
	"gorm.io/gorm"
)

func generateAccountNumber() string {
	source := rand.NewSource(time.Now().UnixNano())
	random := rand.New(source)

	// * Generate 14-digit random Number between [10000000000000,90000000000000]
	min := int64(10000000000000)
	max := int64(90000000000000)

	randomNumber := min + random.Int63n(max-min+1)

	return strconv.FormatInt(randomNumber, 10)
}

func AddCustomer(ctx *gin.Context) {
	if logger == nil {
		ctx.JSON(http.StatusInternalServerError, gin.H{"message": "Internal server error: Logger not initialized"})
	}
	var customer models.Customers
	if err := ctx.ShouldBindJSON(&customer); err != nil {
		logger.Error("Invalid request payload", zap.Error(err))
		ctx.JSON(http.StatusBadRequest, gin.H{"message": "Invalid request payload", "error": err.Error()})
		return
	}
	logger.Info("Received Customer Request", zap.String("FirstName: ", customer.FirstName))
	fmt.Println(customer.DateOfBirth)

	// ? Check for existing customer
	var existingCustomer models.Customers

	customerNotFoundError := bankAppDbConnector.Where("email = ?", customer.Email).First(&existingCustomer).Error

	if customerNotFoundError == gorm.ErrRecordNotFound {

		// * Generate Account Number
		accountNumber := generateAccountNumber()

		newCustomer := &models.Customers{
			FirstName:     customer.FirstName,
			LastName:      customer.LastName,
			Address:       customer.Address,
			AccountNumber: accountNumber,
			Email:         customer.Email,
			City:          customer.City,
			DateOfBirth:   customer.DateOfBirth,
			Phone:         customer.Phone,
		}

		primaryKey := bankAppDbConnector.Create(newCustomer)

		if primaryKey.Error != nil {
			logger.Error("Failed to Add Customer", zap.Error(primaryKey.Error))
			ctx.JSON(http.StatusBadRequest, gin.H{"message": primaryKey.Error.Error()})
			return
		}

		logger.Info(fmt.Sprintf("Customer %s created successfully", customer.FirstName))
		ctx.JSON(http.StatusCreated, gin.H{"message": "Customer Created Successfully!"})
	} else {
		logger.Warn("Customer Already exist", zap.String("email", customer.Email))
		ctx.JSON(http.StatusConflict, gin.H{"message": "Customer Already Exist"})
	}
}
