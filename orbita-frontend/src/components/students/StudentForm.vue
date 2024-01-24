<script setup lang="ts">
import { reactive } from 'vue'
import { vMaska } from 'maska'
import { StudentService } from '@/services/external'
import { CpfMaskOptions } from '@/utils/CpfMask'
import useVuelidate from '@vuelidate/core'
import { email, helpers, maxLength, minLength, required } from '@vuelidate/validators'
import { showSnackbar } from '@/services/SnackbarService'
import { useRouter } from 'vue-router'
import type { Student } from '@/models/Student'
import { watch } from 'vue'

const router = useRouter()

const props = defineProps<{ student?: Student }>()

var studentState = reactive({
  name: '',
  email: '',
  ra: '',
  cpf: ''
})
var $externalErrors = reactive({})

const v$ = useVuelidate(
  {
    name: { required: helpers.withMessage('O campo Nome é obrigatório', required) },
    email: {
      required: helpers.withMessage('O campo Email é obrigatório', required),
      email: helpers.withMessage('Não é um email válido', email)
    },
    ra: { required: helpers.withMessage('O campo RA é obrigatório', required) },
    cpf: {
      required: helpers.withMessage('O campo CPF é obrigatório', required),
      minLength: helpers.withMessage('O CPF não é válido', minLength(14)),
      maxLength: helpers.withMessage('O CPF não é válido', maxLength(14))
    }
  },
  studentState,
  { $externalResults: $externalErrors }
)

watch(
  () => props.student,
  (newStudent) => {
    if (!newStudent) return
    studentState.name = newStudent.name
    studentState.cpf = newStudent.cpf
    studentState.email = newStudent.email
    studentState.ra = newStudent.ra
  },
  { deep: true, immediate: true }
)

async function saveStudent() {
  const isFormCorrect = await v$.value.$validate()
  if (!isFormCorrect) return

  const studentPayload = { ...studentState, cpf: studentState.cpf.replace(/\D/g, '') }

  if (props.student?.id && props.student.id > 0) {
    StudentService.putStudent(props.student?.id, { ...studentPayload, id: props.student.id })
      .then(() => {
        showSnackbar('Salvo com Sucesso!')
      })
      .catch((error) => {
        if (error.response) {
          Object.assign($externalErrors, error.response.data.errors)
        }
      })
  } else {
    StudentService.postStudent(studentPayload)
      .then((result) => {
        showSnackbar('Salvo com Sucesso!')
        router.push(`/alunos/editar/${result.id}`)
      })
      .catch((error) => {
        if (error.response) {
          Object.assign($externalErrors, error.response.data.errors)
        }
      })
  }
}
</script>

<template>
  <v-form @submit.prevent="saveStudent" validate-on="input">
    <v-text-field
      class="mb-3"
      label="Nome"
      placeholder="Informe o nome completo"
      variant="outlined"
      v-model="studentState.name"
      required
      :error-messages="v$.name.$errors.map((e) => e.$message.toString())"
      @input="v$.name.$touch"
      @blur="v$.name.$touch"
    />
    <v-text-field
      class="mb-3"
      label="E-mail"
      placeholder="Informe apenas um email"
      variant="outlined"
      type="email"
      v-model="studentState.email"
      required
      :error-messages="v$.email.$errors.map((e) => e.$message.toString())"
      @input="v$.email.$touch"
      @blur="v$.email.$touch"
    />
    <v-text-field
      class="mb-3"
      label="RA"
      placeholder="Informe registro acadêmico"
      variant="outlined"
      v-model="studentState.ra"
      required
      :disabled="!!props.student?.id"
      :error-messages="v$.ra.$errors.map((e) => e.$message.toString())"
      @input="v$.ra.$touch"
      @blur="v$.ra.$touch"
    />
    <v-text-field
      class="mb-3"
      label="CPF"
      placeholder="Informe o número do documento"
      variant="outlined"
      v-maska:[CpfMaskOptions]
      v-model="studentState.cpf"
      required
      :error-messages="v$.cpf.$errors.map((e) => e.$message.toString())"
      @input="v$.cpf.$touch"
      @blur="v$.cpf.$touch"
    />
    <v-btn color="primary" class="mr-3" type="submit">Salvar</v-btn>
    <v-btn to="/alunos">Cancelar</v-btn>
  </v-form>
</template>
