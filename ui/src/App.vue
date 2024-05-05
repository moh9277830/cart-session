<script lang="ts">
import { defineComponent, onMounted } from 'vue'
import { useLayout } from './composables/useLayout'

export default defineComponent({
  name: 'App',
  setup() {
    const layout = useLayout()

    onMounted(() => {
      // layout.setLayout('default')
    })

    return { 
      layoutComp: layout.layoutComponent
    }
  }
})
</script>

<template>
  <component :is="layoutComp?.component">
    <router-view />
  </component>
</template>
