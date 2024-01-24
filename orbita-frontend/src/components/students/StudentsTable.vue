<script setup lang="ts">
import type { Student } from '@/models/Student'
import { StudentService } from '@/services/external'
import type { TableHeaders } from '@/types/DataTableHeaders'
import { CpfMask } from '@/utils/CpfMask'
import { mdiPencil, mdiPlus } from '@mdi/js'
import { onMounted } from 'vue'
import { ref } from 'vue'
import StudentDeleteDialog from './StudentDeleteDialog.vue'
import { showSnackbar } from '@/services/SnackbarService'

function fetchStudents() {
  StudentService.getStudents(search.value)
    .then((data) => (students.value = data))
    .catch(() => showSnackbar('Ocorreu um erro ao buscar os alunos'))
}

const headers: TableHeaders = [
  {
    title: 'Registro Acadêmico',
    key: 'ra'
  },
  {
    title: 'Nome',
    key: 'name'
  },
  {
    title: 'CPF',
    key: 'cpf',
    value(item) {
      return CpfMask.masked(item.cpf)
    }
  },
  {
    title: 'Ações',
    key: 'actions',
    sortable: false
  }
]

const search = ref('')

const students = ref<Student[]>([])

onMounted(() => fetchStudents())
</script>

<template>
  <v-row>
    <v-col cols="8">
      <v-form class="d-flex" @submit.prevent="fetchStudents">
        <v-text-field
          class="flex-grow-1 mr-2"
          label="Pesquisar"
          v-model="search"
          variant="solo"
          density="compact"
        ></v-text-field>
        <v-btn color="primary" class="mt-1" type="submit">Pesquisar</v-btn>
      </v-form>
    </v-col>
    <v-col>
      <v-btn class="mt-1" :prepend-icon="mdiPlus" to="/alunos/cadastrar">Cadastrar Aluno</v-btn>
    </v-col>
  </v-row>
  <v-data-table
    :headers="headers"
    :items="students"
    :loading="false"
    item-value="name"
    :header-props="{ class: 'font-weight-bold' }"
  >
    <template v-slot:[`item.actions`]="{ item }">
      <div class="d-inline-flex ga-1">
        <v-btn
          :to="'/alunos/editar/' + item.id"
          :prepend-icon="mdiPencil"
          density="compact"
          size="small"
          variant="plain"
          color="primary"
          >Editar
        </v-btn>
        <student-delete-dialog
          :student="item"
          @student-removed="
            (removed) => {
              if (removed) fetchStudents()
            }
          "
        />
      </div>
    </template>
  </v-data-table>
</template>
