package controller

import "github.com/labstack/echo/v4"

func Server() {
	handler := NewHandler()

	e := echo.New()
	e.GET("/join-code", handler.GetJoinCode)
	e.POST("/join-code", handler.PostJoinCode)

	e.Logger.Fatal(e.Start(":8080"))
}
