package models

import (
	"gorm.io/gorm"
)

type Customers struct {
	gorm.Model
	FirstName     string `gorm:"type:varchar(100)"`
	LastName      string
	Address       string
	City          string
	DateOfBirth   string `json:"dateOfBirth"`
	Phone         string `gorm:"unique"`
	Email         string `gorm:"unique"`
	AccountNumber string
}
