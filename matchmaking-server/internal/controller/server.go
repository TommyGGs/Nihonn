package controller

import (
	"log"
	"net/http"

	"github.com/gorilla/websocket"
)

func Server() {
	ms := NewMatchmakingSystem()
	upgrader := websocket.Upgrader{
		CheckOrigin: func(r *http.Request) bool { return true },
	}

	http.HandleFunc("/hello", func(w http.ResponseWriter, r *http.Request) {
		w.Write([]byte("Hello, World!"))
	})

	http.HandleFunc("/ws", func(w http.ResponseWriter, r *http.Request) {
		conn, err := upgrader.Upgrade(w, r, nil)
		if err != nil {
			log.Println(err)
			return
		}
		ms.HandleConn(conn)
	})

	log.Println("Server started on localhost:8080")
	http.ListenAndServe(":8080", nil)
}
