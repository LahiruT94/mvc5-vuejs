<template>
    <div class="clients-index">

        <h1>Клиенты</h1>

        <div class="table-header-group">
            <el-input type="query" v-model="filter" @change="filterChanged" placeholder="Search..."></el-input>
            <el-button type="danger" v-show="hasSelectedElements" @click="clearSelected()">Удалить выбранные</el-button>
            <el-button @click="createItem()">Добавить клиента</el-button>
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
                <template slot-scope="scope">
                    <el-button size="small" @click="editItem(scope.row)">Редактировать</el-button>
                    <el-button size="small" type="danger" @click="deleteItem(scope.row)">Удалить</el-button>
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
    import {mapGetters, mapActions} from 'vuex'
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
                filter: '',
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
            ...mapActions('Clients', [
                'getClients',
                'setPageSize',
                'setPage',
                'setOrder',
                'setFilter',
                'deleteMultipleClient',
                'deleteClient',
                'createClient',
                'updateClient'
            ]),
            async reload() {
                await this.getClients()
            },
            async handleSizeChange(value) {
                await this.setPageSize(value)
                this.reload()
            },
            async handleCurrentChange(value) {
                await this.setPage(value)
                this.reload()
            },
            async handleSort({prop, order}) {
                await this.setOrder({prop, order})
                this.reload()
            },
            filterChanged(value) {
                clearTimeout(this.delayTimer)
                this.delayTimer = setTimeout(async () => {
                    await this.setFilter(value)
                    this.reload()
                }, 1000)
            },
            cancel() {
                this.model = {}
                this.closeModal()
            },
            async submit(mode) {
                if (mode === 'create') {
                    await this.createClient(this.model)
                } else if (mode === 'update') {
                    await this.updateClient(this.model)
                }
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

                this.deleteItem(ids)
            },
            handleSelectionChange(val) {
                this.selected = val
            },
            createItem() {
                this.modal.mode = 'create'
                this.model = {}
                this.openModal()
            },
            editItem(row) {
                this.modal.mode = 'update'
                Object.assign(this.model, row)
                this.openModal()
            },
            async deleteItem(row) {
                if (Array.isArray(row)) {
                    await this.deleteMultipleClient({
                        params: {ids: row},
                        paramsSerializer: function (params) {
                            return qs.stringify(params, {arrayFormat: 'repeat'})
                        }
                    })
                } else {
                    await this.deleteClient({
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
