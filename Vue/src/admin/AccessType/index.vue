<template>
    <div class="access-type-index">

        <h1>Типы доступа</h1>

        <div class="table-header-group">
            <el-input type="query" @change="filterChanged" placeholder="Search..."></el-input>
            <el-button type="danger" v-show="hasSelectedElements" @click="clearSelected()">Удалить выбранные</el-button>
            <el-button @click="createAccessType()">Добавить тип доступа</el-button>
        </div>

        <el-table :data="getAccessTypeList" emptyText="Пусто" border style="width: 100%"
                  @selection-change="handleSelectionChange" @sort-change="handleSort">

            <el-table-column type="selection" width="55"></el-table-column>

            <el-table-column property="Id" sortable="custom" label="id" width="120"></el-table-column>

            <el-table-column property="Title" sortable="custom" label="Тип доступа"></el-table-column>

            <el-table-column label="Действия">
                <template scope="scope">
                    <el-button size="small" @click="editAccessType(scope.row)">Редактировать</el-button>
                    <el-button size="small" type="danger" @click="deleteAccessType(scope.row)">Удалить</el-button>
                </template>
            </el-table-column>

        </el-table>

        <el-pagination @size-change="handleSizeChange" @current-change="handleCurrentChange"
                       :current-page="getCurrentPage" :page-sizes="[10,25,50,100]" :page-size="getPageSize"
                       layout="total, sizes, prev, pager, next, jumper" :total="getTotalItems"></el-pagination>
        <access-type-modal-form :mode="modal.mode" :model="model" :modal="modal" @cancel="cancel"
                                @submit="submit"></access-type-modal-form>
    </div>
</template>


<script>
    import AccessTypeModalForm from '@admin/AccessType/AccessTypeModalForm.vue'
    import {mapGetters} from 'vuex'
    import qs from 'qs'

    export default {
        name: 'access-type-index',
        components: {
            'access-type-modal-form': AccessTypeModalForm
        },
        async beforeCreate() {
            await this.$store.dispatch('AccessType/getAccessTypes')
        },
        data() {
            return {
                selected: [],
                model: {
                    Title: ''
                },
                modal: {
                    show: false,
                    mode: 'create'
                }
            }
        },
        computed: {
            ...mapGetters('AccessType', [
                'getAccessTypeList',
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
                await this.$store.dispatch('AccessType/getAccessTypes')
            },
            async handleSizeChange(value) {
                await this.$store.dispatch('AccessType/setPageSize', value)
                this.reload()
            },
            async handleCurrentChange(value) {
                await this.$store.dispatch('AccessType/setPage', value)
                this.reload()
            },
            async handleSort({prop, order}) {
                await this.$store.dispatch('AccessType/setOrder', {prop, order})
                this.reload()
            },
            filterChanged(value) {
                clearTimeout(this.delayTimer)
                this.delayTimer = setTimeout(async () => {
                    await this.$store.dispatch('AccessType/setFilter', value)
                    this.reload()
                }, 1000)
            },
            cancel() {
                this.closeModal()
            },
            async submit(mode) {
                await this.$store.dispatch(`AccessType/${mode}AccessType`, this.model)
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

                this.deleteAccessType(ids)
            },
            handleSelectionChange(val) {
                this.selected = val
            },
            createAccessType() {
                this.modal.mode = 'create'
                this.model = {}
                this.openModal()
            },
            editAccessType(row) {
                this.modal.mode = 'update'
                Object.assign(this.model, row)
                this.openModal()
            },
            async deleteAccessType(row) {
                if (Array.isArray(row)) {
                    await this.$store.dispatch('AccessType/deleteMultipleAccessTypes', {
                        params: {ids: row},
                        paramsSerializer: function (params) {
                            return qs.stringify(params, {arrayFormat: 'repeat'})
                        }
                    })
                } else {
                    await this.$store.dispatch('AccessType/deleteAccessType', {
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
