package controller

import (
	"log"
	"sync"
	"time"

	"github.com/gorilla/websocket"
)

type MatchmakingSystem struct {
	waitingPlayer *websocket.Conn
	mu            sync.Mutex
	activeConns   int
	joinCode      string
}

func NewMatchmakingSystem() *MatchmakingSystem {
	return &MatchmakingSystem{}
}

func (ms *MatchmakingSystem) HandleConn(conn *websocket.Conn) {
	ms.mu.Lock()
	if ms.activeConns >= 2 {
		conn.Close() // 2つの接続が既にある場合は、新しい接続を拒否
		ms.mu.Unlock()
		return
	}
	ms.activeConns++
	ms.mu.Unlock()

	defer func() {
		ms.mu.Lock()
		ms.activeConns--
		ms.mu.Unlock()
		conn.Close()
	}()

	for {
		deadline := time.Now().Add(60 * time.Second)
		err := conn.SetReadDeadline(deadline)
		if err != nil {
			log.Println(err)
			return
		}

		if ms.joinCode != "" {
			if ms.waitingPlayer != nil {
				ms.startMatch(conn, ms.waitingPlayer, ms.joinCode)
				ms.waitingPlayer = nil
				ms.joinCode = ""
			} else {
				ms.waitingPlayer = conn
			}
		}

		_, msg, err := conn.ReadMessage()
		if err != nil {
			log.Println(err)
			return
		}

		ms.joinCode = string(msg)
	}

}

func (ms *MatchmakingSystem) startMatch(player1, player2 *websocket.Conn, joinCode string) {
	ms.sendMatchInfo(player1, joinCode)
	ms.sendMatchInfo(player2, joinCode)
}

func (ms *MatchmakingSystem) sendMatchInfo(conn *websocket.Conn, joinCode string) {
	message := map[string]string{"join_code": joinCode}
	err := conn.WriteJSON(message)
	if err != nil {
		log.Println("Error sending match info:", err)
	}
}
