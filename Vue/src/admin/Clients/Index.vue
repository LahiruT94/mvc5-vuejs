<template>
    <div class="clients-index">

        <h1>Клиенты</h1>

        <div class="table-header-group">
            <el-input type="query" v-model="filter" @change="filterChanged" placeholder="Search..."></el-input>
            <el-button type="danger" v-show="hasSelectedElements" @click="clearSelected()">Удалить выбранные</el-button>
            <el-button @click="createClient()">Добавить клиента</el-button>
        </div>

        <el-table :data="getClientList" emptyText="Пусто" border style="width: 100%"
                  @selection-change="handleSelectionChange" @sort-change="handleSort">

            <el-table-column type="selection" width="55"></el-table-column>
            <el-table-column property="Id" sortable="custom" label="id" width="120"></el-table-column>
            <el-table-column property="Title" sortable="custom" label="Компания"></el-table-column>
            <el-table-column property="Email" sortable="custom" label="Email"></el-table-column>
            <el-table-column property="Phone" sortable="custom" label="Phone"></el-table-column>
            <el-table-column property="Note" sortable="custom" label="Note"></el-table-column>

            <el-table-column label="Действия">
                <template scope="scope">
                    <el-button size="small" @click="editClient(scope.row)">Редактировать</el-button>
                    <el-button size="small" type="danger" @click="deleteClient(scope.row)">Удалить</el-button>
                </template>
            </el-table-column>

        </el-table>

        <el-pagination @size-change="handleSizeChange" @current-change="handleCurrentChange"
                       :current-page="getCurrentPage" :page-sizes="[10,25,50,100]" :page-size="getPageSize"
                       layout="total, sizes, prev, pager, next, jumper" :total="getTotalItems"></el-pagination>
        <client-modal-form :mode="modal.mode" :model="model" :modal="modal" @cancel="cancel"
                           @submit="submit"></client-modal-form>
    </div>
</template>


<script>
    import ClientModalForm from '@admin/Clients/ClientModalForm.vue'
    import {mapGetters} from 'vuex'
    import qs from 'qs'

    export default {
        name: 'clients-index',
        components: {
            'client-modal-form': ClientModalForm
        },
        async beforeCreate() {
            await this.$store.dispatch('Clients/getClients')
        },
        data() {
            return {
                selected: [],
                filter: this.filterKey,
                sortOrder: {},
                model: {
                    Title: '',
                    Email: '',
                    Phone: '',
                    Note: ''
                },
                modal: {
                    show: false,
                    mode: 'create'
                }
            }
        },
        computed: {
            ...mapGetters('Clients', [
                'getClientList',
                'getTotalItems',
                'getCurrentPage',
                'getPageSize'
            ]),
            hasSelectedElements() {
                if (this.selected) return this.selected.length > 0
                return false
            }
        },
        methods: {
            async reload() {
                await this.$store.dispatch('Clients/getClients')
            },
            async handleSizeChange(value) {
                await this.$store.dispatch('Clients/setPageSize', value)
                this.reload()
            },
            async handleCurrentChange(value) {
                await this.$store.dispatch('Clients/setPage', value)
                this.reload()
            },
            async handleSort({prop, order}) {
                await this.$store.dispatch('Clients/setOrder', {prop, order})
                this.reload()
            },
            filterChanged(value) {
                clearTimeout(this.delayTimer)
                this.delayTimer = setTimeout(async () => {
                    await this.$store.dispatch('Clients/setFilter', value)
                    this.reload()
                }, 1000)
            },
            cancel() {
                this.closeModal()
            },
            async submit(mode) {
                await this.$store.dispatch(`Clients/${mode}Client`, this.model)
                this.closeModal()
            },
            openModal() {
                this.modal.show = true
            },
            closeModal() {
                this.modal.show = false
            },
            clearSelected() {
                let ids = []

                this.selected.forEach(row => {
                    ids.push(row.Id)
                }, 0)

                this.deleteClient(ids)
            },
            handleSelectionChange(val) {
                this.selected = val
            },
            createClient() {
                this.modal.mode = 'create'
                this.model = {}
                this.openModal()
            },
            editClient(row) {
                this.modal.mode = 'update'
                Object.assign(this.model, row)
                this.openModal()
            },
            async deleteClient(row) {
                if (Array.isArray(row)) {
                    await this.$store.dispatch('Clients/deleteMultipleClient', {
                        params: {ids: row},
                        paramsSerializer: function (params) {
                            return qs.stringify(params, {arrayFormat: 'repeat'})
                        }
                    })
                } else {
                    await this.$store.dispatch('Clients/deleteClient', {
                        params: {id: row.Id}
                    })
                }
            }
        }
    }
</script>

<style lang="sass">
    .table-header-group
        width: 50%
        display: inline-flex
        margin: 5px
        .table-header-group > *
            margin-right: 10px
</style>
