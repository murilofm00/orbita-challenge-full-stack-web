<script setup lang="ts">
import { mdiAccountSchool } from '@mdi/js'
import { onMounted } from 'vue'
import { ref } from 'vue'
import { useDisplay } from 'vuetify'

const { mobile, mdAndDown } = useDisplay()

var drawer = ref(false)

onMounted(() => {
  if (!mobile.value) drawer.value = true
})
</script>

<template>
  <v-app>
    <v-app-bar color="primary" v-if="mdAndDown">
      <v-app-bar-nav-icon variant="text" @click.stop="drawer = !drawer"></v-app-bar-nav-icon>
      <v-toolbar-title>+A Educação</v-toolbar-title>
    </v-app-bar>

    <v-navigation-drawer v-model="drawer">
      <v-list>
        <v-list-item>
          <div class="d-flex justify-center my-4">
            <img height="100" src="@/assets/logo-mais-a-educacao.svg" />
          </div>
        </v-list-item>
        <v-list-subheader>Módulo Academico</v-list-subheader>
        <v-list-item :prepend-icon="mdiAccountSchool" title="Alunos" link to="/alunos" />
      </v-list>
    </v-navigation-drawer>
    <v-main>
      <RouterView />
    </v-main>
  </v-app>
</template>
