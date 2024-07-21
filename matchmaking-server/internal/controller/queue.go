package controller

type Queue struct {
	data []string
	size int
}

func NewQueue(cap int) *Queue {
	return &Queue{data: make([]string, 0, cap), size: 0}
}

func (q *Queue) Enqueue(value string) {
	q.data = append(q.data, value)
	q.size++
}

func (q *Queue) Dequeue() string {
	if q.size == 0 {
		return ""
	}
	value := q.data[0]
	q.data = q.data[1:]
	q.size--
	return value
}

func (q *Queue) IsEmpty() bool {
	return q.size == 0
}
