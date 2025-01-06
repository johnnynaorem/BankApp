package main

import (
	"myModule/config"
	"myModule/models"
	"net/http"

	"github.com/gin-contrib/cors"
	"github.com/gin-gonic/gin"
	"github.com/joho/godotenv"
	"go.uber.org/zap"
	"gorm.io/gorm"
)

var logger *zap.Logger
var bankAppDbConnector *gorm.DB

func init() {
	var err error
	logger, err = zap.NewDevelopment()
	if err != nil {
		panic("Failed to initialize logger: " + err.Error())
	}
	defer logger.Sync()
}

func main() {

	// ? Configure the env File
	if err := godotenv.Load(".env"); err != nil {
		panic("No .env file found")
	}

	bankAppDbConnector = config.ConnectDB()

	// ? configuration of the http server.
	httpServer := gin.Default()

	// ? Apply CORS middleware
	httpServer.Use(cors.New(cors.Config{
		AllowOrigins:     []string{"*"},                                       // Allow all origins
		AllowMethods:     []string{"GET", "POST", "PUT", "DELETE"},            // Allowed HTTP methods
		AllowHeaders:     []string{"Origin", "Content-Type", "Authorization"}, // Allowed headers
		AllowCredentials: true,                                                // Allow credentials
	}))

	// * Unprotected Routes
	httpServer.POST("/add-customer", AddCustomer)
	httpServer.GET("/get-customers", func(ctx *gin.Context) {
		var customers []models.Customers
		if err := bankAppDbConnector.Find(&customers).Error; err != nil {
			ctx.JSON(http.StatusInternalServerError, gin.H{"error": "Unable to retrieve customers"})
			return
		}
		ctx.JSON(http.StatusOK, gin.H{"customer": customers})
	})

	// ? running the server
	httpServer.Run(":8080")
}
