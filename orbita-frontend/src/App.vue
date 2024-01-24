<script setup lang="ts">
import { RouterView } from 'vue-router'
import { snackbar } from '@/services/SnackbarService'
</script>

<template>
  <RouterView />
  <v-snackbar v-model="snackbar.show" :color="snackbar.color">
    {{ snackbar.message }}
  </v-snackbar>
</template>
