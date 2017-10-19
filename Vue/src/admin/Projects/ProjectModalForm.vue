<template>
    <el-dialog v-bind:title="text.Title" :visible.sync="modal.show">
        <form class="prject-form">
            <div class="form-group">
                <label for="Title">Title</label>
                <input v-model.trim="model.Title" type="text" class="form-control" id="Title">
            </div>
            <div class="form-group">
                <label for="Client">Client</label>
                <v-select id="Client" :debounce="250" v-model="model.Client" :on-search="filterChanged" :options="clients" placeholder="Начните вводить..." label="Title">
                </v-select>
            </div>
        </form>
        <span slot="footer" class="dialog-footer">
            <el-button @click="$emit('cancel')">Отмена</el-button>
            <el-button type="primary" @click="$emit('submit', mode)" v-text="text.Button"></el-button>
        </span>
    </el-dialog>
</template>

<script>
import { mapGetters } from 'vuex'

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
                const valid = ['create', 'update'];
                if (valid.indexOf(value.toLowerCase()) >= 0) {
                    return true;
                }
                return false;
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
    created() {
        this.$store.dispatch('Clients/getClients')
            .then(() => {
                this.clients = this.getClientList.map(item => {
                    return { Id: item.Id, Title: item.Title };
                })
            });
    },
    methods: {
        filterChanged(search, loading) {
            loading(true)
            setTimeout(() => {
                this.$store.dispatch('Clients/setFilter', search)
                this.$store.dispatch('Clients/getClients')
                    .then(() => {
                        this.clients = this.getClientList.map(item => {
                            return { Id: item.Id, Title: item.Title };
                        })
                        loading(false)
                    })
            }, 500);
        },
    }
}
</script>

