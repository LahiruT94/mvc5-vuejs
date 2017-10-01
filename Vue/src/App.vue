<template>
  <div id="app">
    <router-view :data="gridData">
    </router-view>   
  </div>
</template>

<script>

export default {
  name: 'app',
  data() {
    return {
      gridData: [
        { name: 'Chuck Norris', power: Infinity },
        { name: 'Bruce Lee', power: 9001 },
        { name: 'Jackie Chan', power: 7000 },
        { name: 'Jet Li', power: 8000 }
      ]
    }
  }
}
</script>

<style>
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
