<template>
    <el-dialog v-bind:title="text.Title" :visible.sync="modal.show">
        <form class="project-form">
            <div class="form-group">
                <label>Title
                    <input v-model.trim="model.Title" class="form-control">
                </label>
            </div>
            <div class="form-group">
                <label>Client
                    <v-select :debounce="250" v-model="model.Client" :on-search="filterChanged"
                              :options="clients" placeholder="Начните вводить..." label="Title">
                    </v-select>
                </label>
            </div>
        </form>
        <span slot="footer" class="dialog-footer">
            <el-button @click="$emit('cancel')">Отмена</el-button>
            <el-button type="primary" @click="$emit('submit', mode)" v-text="text.Button"></el-button>
        </span>
    </el-dialog>
</template>

<script>
    import {mapGetters} from 'vuex'

    export default {
        props: {
            model: {
                type: Object,
                required: true
            },
            modal: {
                type: Object,
                required: true,
                default() {
                    return {
                        show: false
                    }
                }
            },
            mode: {
                type: String,
                required: true,
                default: 'create',
                validator(value) {
                    const valid = ['create', 'update']
                    return valid.indexOf(value.toLowerCase()) >= 0
                }
            },
        },
        data() {
            return {
                clients: [],
            }
        },
        computed: {
            ...mapGetters('Clients', ['getClientList']),
            text() {
                let create = {
                    Title: 'Добавить проект',
                    Button: 'Добавить'
                }
                let update = {
                    Title: 'Редактировать проект',
                    Button: 'Сохранить'
                }

                if (this.mode === 'update')
                    return update
                else
                    return create
            }
        },
        async created() {
            await this.$store.dispatch('Clients/getClients')
            this.clients = this.getClientList.map(item => {
                return {Id: item.Id, Title: item.Title}
            })
        },
        methods: {
            filterChanged(search, loading) {
                loading(true)
                setTimeout(async () => {
                    await this.$store.dispatch('Clients/setFilter', search)
                    await this.$store.dispatch('Clients/getClients')
                    this.clients = this.getClientList.map(item => {
                        return {Id: item.Id, Title: item.Title}
                    })
                    loading(false)
                }, 500)
            },
        }
    }
</script>

