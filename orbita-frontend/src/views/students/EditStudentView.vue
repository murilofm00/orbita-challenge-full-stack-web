<script setup lang="ts">
import type { Student } from '@/models/Student'
import { showSnackbar } from '@/services/SnackbarService'
import { StudentService } from '@/services/external'
import { reactive, watch } from 'vue'
import PageTitle from '@/components/PageTitle.vue'
import StudentForm from '@/components/students/StudentForm.vue'

const props = withDefaults(defineProps<{ id: number }>(), { id: 0 })

const student = reactive<Student>({} as Student)

function getStudent(id: number) {
  StudentService.getStudent(id)
    .then((result) => Object.assign(student, result))
    .catch(() => showSnackbar('Ocorreu um erro ao obter o aluno.', 'error'))
}

watch(
  () => props.id,
  (id) => {
    if (id > 0) getStudent(id)
  },
  { immediate: true }
)
</script>

<template>
  <page-title>
    Editar Aluno <span class="font-weight-thin text-h4">({{ student.ra }})</span>
  </page-title>
  <v-container>
    <student-form :student="student" />
  </v-container>
</template>
