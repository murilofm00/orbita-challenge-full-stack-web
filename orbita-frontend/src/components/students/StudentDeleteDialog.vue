<script setup lang="ts">
import { mdiDelete } from '@mdi/js'
import { CpfMask } from '@/utils/CpfMask'
import type { Student } from '@/models/Student'
import { StudentService } from '@/services/external'
import { showSnackbar } from '@/services/SnackbarService'
import { ref } from 'vue'

const props = defineProps<{ student: Student }>()
const emit = defineEmits<{
  (e: 'studentRemoved', removed: boolean): void
}>()

function removerAluno() {
  StudentService.deleteStudent(props.student.id)
    .then(() => {
      showSnackbar('O aluno foi removido.')
      dialog.value = false
      emit('studentRemoved', true)
    })
    .catch(() => {
      showSnackbar('Ocorreu um erro ao remover o aluno.', 'error')
      dialog.value = false
      emit('studentRemoved', false)
    })
}

const dialog = ref(false)
</script>

<template>
  <v-dialog width="750" v-model="dialog">
    <template v-slot:activator="{ props }">
      <v-btn
        :prepend-icon="mdiDelete"
        density="compact"
        size="small"
        variant="plain"
        color="error"
        v-bind="props"
      >
        Excluir
      </v-btn>
    </template>

    <template v-slot:default="{ isActive }">
      <v-card title="Deseja remover o seguinte aluno?">
        <v-card-text>
          <v-list>
            <v-list-item
              ><span class="font-weight-bold">Nome: </span> {{ props.student.name }}</v-list-item
            >
            <v-list-item
              ><span class="font-weight-bold">Email: </span> {{ props.student.email }}</v-list-item
            >
            <v-list-item
              ><span class="font-weight-bold">RA: </span> {{ props.student.ra }}</v-list-item
            >
            <v-list-item>
              <span class="font-weight-bold">CPF: </span>
              {{ CpfMask.masked(props.student.cpf || '') }}
            </v-list-item>
          </v-list>
        </v-card-text>

        <v-card-actions>
          <v-spacer></v-spacer>

          <v-btn
            text="Remover"
            color="error"
            :prepend-icon="mdiDelete"
            variant="flat"
            @click="removerAluno"
          ></v-btn>
          <v-btn text="Cancelar" variant="flat" @click="isActive.value = false"></v-btn>
        </v-card-actions>
      </v-card>
    </template>
  </v-dialog>
</template>
