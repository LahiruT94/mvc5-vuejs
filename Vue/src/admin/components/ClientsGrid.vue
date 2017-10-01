<template>
    <div class="awesome-class">
        {{awesome}}
       
    </div>
</template>


<script>

export default {
    name: 'clientsGrid',
    data() {
        return {
            awesome: "text1",
            clients: [],
            gridData: {
                data: this.data,
                selected: [],
            },
        }
    },
    computed: {
    },
    created() {
        this.$http.get('api/client')
            .then(response => this.clients = response.data.clientList);
    },
    methods: {
    }
}
</script>

<style lang="sass">
.awesome-class
    font-size: 15px
    margin-top: 60px  
</style>
