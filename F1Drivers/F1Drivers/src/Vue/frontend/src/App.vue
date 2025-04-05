<template>
  <div id="app">
    <h1>Welcome to the Driver Grid</h1>
    <GridComponent />
  </div>
</template>

<script lang="ts">
import { Options, Vue } from 'vue-class-component';
import GridComponent from './components/GridComponent.vue';

@Options({
  components: {
    GridComponent,
  },
})
export default class App extends Vue {}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
