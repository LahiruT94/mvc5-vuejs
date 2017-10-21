<template>
    <div class="projects-index">

        <h1>Проекты</h1>

        <div class="table-header-group">
            <el-input type="query" v-model="filter" @change="filterChanged" placeholder="Search..."></el-input>
            <el-button type="danger" v-show="hasSelectedElements" @click="clearSelected()">Удалить выбранные</el-button>
            <el-button @click="createProject()">Добавить проект</el-button>
        </div>

        <el-table :data="getProjectList" border style="width: 100%" @selection-change="handleSelectionChange"
                  @sort-change="handleSort">

            <el-table-column type="selection" width="55"></el-table-column>
            <el-table-column type="expand" label="Клиент">
                <template scope="props">
                    <div>
                        <p>Id: {{props.row.Client.Id}}</p>
                        <p>Name: {{props.row.Client.Title}}</p>
                        <p>Email: {{props.row.Client.Email}}</p>
                        <p>Phone: {{props.row.Client.Phone}}</p>
                    </div>
                </template>
            </el-table-column>
            <el-table-column property="Id" sortable="custom" label="id" width="120"></el-table-column>
            <el-table-column property="Title" sortable="custom" label="Название проекта"></el-table-column>

            <el-table-column label="Действия">
                <template scope="scope">
                    <el-button size="small" @click="editProject(scope.row)">Редактировать</el-button>
                    <el-button size="small" type="danger" @click="deleteProject(scope.row)">Удалить</el-button>
                </template>
            </el-table-column>

        </el-table>

        <el-pagination @size-change="handleSizeChange" @current-change="handleCurrentChange"
                       :current-page="getCurrentPage" :page-sizes="[10,25,50,100]" :page-size="getPageSize"
                       layout="total, sizes, prev, pager, next, jumper" :total="getTotalItems"></el-pagination>
        <project-modal-form :mode="modal.mode" :model="model" :modal="modal" @cancel="cancel"
                          @submit="submit"></project-modal-form>
    </div>
</template>

<script>
    import ProjectModalForm from '@admin/Projects/ProjectModalForm.vue'
    import {mapGetters} from 'vuex'
    import qs from 'qs'

    export default {
        name: 'project-index',
        components: {
            'project-modal-form': ProjectModalForm
        },
        async beforeCreate() {
            await this.$store.dispatch('Projects/getProjects')
        },
        data() {
            return {
                selected: [],
                filter: this.filterKey,
                sortOrder: {},
                model: {
                    Title: '',
                    Id: 0,
                    ClientId: 0
                },
                modal: {
                    show: false,
                    mode: 'create'
                }
            }
        },
        computed: {
            ...mapGetters('Projects', [
                'getProjectList',
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
                await this.$store.dispatch('Projects/getProjects')
            },
            async handleSizeChange(value) {
                await this.$store.dispatch('Projects/setPageSize', value)
                this.reload()
            },
            async handleCurrentChange(value) {
                await this.$store.dispatch('Projects/setPage', value)
                this.reload()
            },
            async handleSort({prop, order}) {
                await this.$store.dispatch('Projects/setOrder', {prop, order})
                this.reload()
            },
            filterChanged(value) {
                clearTimeout(this.delayTimer)
                this.delayTimer = setTimeout(async () => {
                    await this.$store.dispatch('Projects/setFilter', value)
                    this.reload()
                }, 700)
            },
            cancel() {
                this.closeModal()
            },
            async submit(mode) {
                await this.$store.dispatch(`Projects/${mode}Project`, this.model)
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

                this.deleteProject(ids)
            },
            handleSelectionChange(val) {
                this.selected = val
            },
            createProject() {
                this.modal.mode = 'create'
                this.model = {}
                this.openModal()
            },
            editProject(row) {
                this.modal.mode = 'update'
                this.model.Id = row.Id
                this.model.Title = row.Title
                this.model.Client = {Id: row.Client.Id, Title: row.Client.Title}
                this.openModal()
            },
            async deleteProject(row) {
                if (Array.isArray(row)) {
                    await this.$store.dispatch('Projects/deleteMultipleProjects', {
                        params: {ids: row},
                        paramsSerializer: function (params) {
                            return qs.stringify(params, {arrayFormat: 'repeat'})
                        }
                    })
                } else {
                    await this.$store.dispatch('Projects/deleteProject', {
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
