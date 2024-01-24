<script setup lang="ts">
import StudentsTable from '@/components/students/StudentsTable.vue'
</script>

<template>
  <div class="mt-4 ml-4">
    <h2 class="text-h2">Consulta de alunos</h2>
    <v-divider class="my-2" />
  </div>
  <v-container>
    <students-table></students-table>
  </v-container>
</template>
