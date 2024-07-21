package controller

import "github.com/labstack/echo/v4"

type Handler struct {
	Queue *Queue
}

func NewHandler() *Handler {
	cap := 100
	return &Handler{
		Queue: NewQueue(cap),
	}
}

func (h *Handler) GetJoinCode(ctx echo.Context) error {
	joinCode := h.Queue.Dequeue()

	return ctx.String(200, joinCode)
}

func (h *Handler) PostJoinCode(ctx echo.Context) error {
	joinCode := ctx.FormValue("joinCode")
	h.Queue.Enqueue(joinCode)

	return ctx.String(200, "OK")
}
