import { reactive } from 'vue'

export const snackbar = reactive({ show: false, message: '', color: '' })

export function showSnackbar(message: string, color = 'success') {
  snackbar.show = true
  snackbar.message = message
  snackbar.color = color
}
