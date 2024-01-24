import { api } from './api'
import { type Student } from '@/models/Student'

export async function getStudents(search?: string) {
  const { data } = await api.get<Student[]>('/api/students', { params: { search } })
  return data
}

export async function getStudent(id: number) {
  const { data } = await api.get<Student>(`/api/students/${id}`)
  return data
}

export async function postStudent(student: Omit<Student, 'id'>) {
  const { data } = await api.post('/api/students', student)
  return data
}

export async function putStudent(id: number, student: Student) {
  const { data } = await api.put(`/api/students/${id}`, student)
  return data
}

export async function deleteStudent(id: number) {
  const { data } = await api.delete(`/api/students/${id}`)
  return data
}
