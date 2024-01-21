<script setup lang="ts">
import { mdiAccountSchool } from '@mdi/js'
</script>

<template>
  <v-app>
    <v-navigation-drawer>
      <v-list>
        <v-list-item>
          <div class="d-flex justify-center my-4">
            <img
            height="100"
            src="@/assets/logo-mais-a-educacao.svg"
            />
          </div>
        </v-list-item>
        <v-list-subheader>Módulo Academico</v-list-subheader>
        <v-list-item :prepend-icon="mdiAccountSchool" title="Alunos" link href="alunos" />
      </v-list>
    </v-navigation-drawer>
    <v-main>
      <RouterView />
    </v-main>
  </v-app>
</template>
