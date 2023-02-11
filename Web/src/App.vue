<template>
    <!--<Home></Home>-->
    <AddUser />
</template>

<script>
//import HelloWorld from './components/HelloWorld.vue'
import Home from './components/Home.vue'
import AddUser from './components/AddUser.vue'

    export default {
  name: 'App',
  components: {
      Home,
      AddUser
  }
}
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
