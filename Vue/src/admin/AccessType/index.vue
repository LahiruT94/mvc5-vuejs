<template>
    <div class="access-type-index">

        <h1>Типы доступа</h1>

        <div>
            <el-button v-show="hasSelectedElements" @click="clearSelected()">Удалить выбранные</el-button>
            <el-button @click="handleCreate()">Добавить тип доступа</el-button>
        </div>

        <el-input type="query" v-model="filter" placeholder="Search..."></el-input>

        <el-table :data="filteredData" emptyText="Пусто" border style="width: 100%" @selection-change="handleSelectionChange">

            <el-table-column type="selection" width="55"></el-table-column>

            <el-table-column property="Id" sortable label="id" width="120"></el-table-column>

            <el-table-column property="Title" sortable label="Тип доступа" width="120"></el-table-column>

            <el-table-column label="Действия">
                <template scope="scope">
                    <el-button size="small" @click="editRow(scope.row)">Редактировать</el-button>
                    <el-button size="small" type="danger" @click="deleteRow(scope.row)">Удалить</el-button>
                </template>
            </el-table-column>

        </el-table>

        <el-dialog title="Добавить тип доступа" :visible.sync="modal.show">
            <component :is="modal.mode" :model="model" />
            <span slot="footer" class="dialog-footer">
                <el-button @click="closeModal()">Cancel</el-button>
                <el-button type="primary" @click="confirmModal()">Confirm</el-button>
            </span>
        </el-dialog>
    </div>
</template>


<script>
import create from '@admin/AccessType/create.vue'
import { EventBus } from './utilities/event-bus';
import update from '@admin/AccessType/update.vue'
import qs from 'qs'
export default {
    name: 'index',
    components: {
        create, update
    },
    beforeCreate() {
        this.$http.get('api/AccessType')
            .then((response) => {
                this.data = response.data.accessTypeList
            })
            .catch((error) => {
                window.console.error(error);
            });

        EventBus.$on('created', () => {
            this.reloadTable()
        })
        EventBus.$on('updated', () => {
            this.reloadTable()
        })
    },
    data() {
        let sortOrders = {}
        if (this.data)
            Object.keys(this.data).forEach((key) => {
                sortOrders[key] = 1
            })
        return {
            sortKey: '',
            sortOrders: sortOrders,
            data: [],
            selected: [],
            filter: this.filterKey,
            model: { Title: "" },
            formLabelWidth: '50px',
            modal: {
                show: false,
                mode: ''
            }
        }
    },
    computed: {
        filteredData() {
            let filterKey = this.filter && this.filter.toLowerCase()
            let order = this.sortOrders[this.sortKey] || 1
            let data = this.data
            if (filterKey) {
                data = data.filter((row) => {
                    return Object.keys(row).some((key) => {
                        return String(row[key]).toLowerCase().indexOf(filterKey) > -1
                    })
                })
            }
            if (this.sortKey) {
                data = data.slice().sort((a, b) => {
                    a = a[this.sortKey]
                    b = b[this.sortKey]
                    return (a === b ? 0 : a > b ? 1 : -1) * order
                })
            }
            return data
        },
        hasSelectedElements() {
            if (this.selected)
                return this.selected.length > 0
            return false
        }
    },
    methods: {
        sortBy(key) {
            this.sortKey = key
            this.sortOrders[key] = this.sortOrders[key] * -1
        },
        reloadTable() {
            this.$http.get('api/AccessType')
                .then((response) => {
                    this.data = response.data.accessTypeList
                })
                .catch((error) => {
                    window.console.error(error);
                });
        },
        confirmModal() {
            EventBus.$emit('submitted');
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

            this.selected.forEach((row) => {
                ids.push(row.Id);
            }, 0);

            this.deleteRow(ids)
        },
        handleSelectionChange(val) {
            this.selected = val
        },
        handleCreate() {
            this.modal.mode = 'create'
            this.model = {}
            this.openModal()
        },
        editRow(row) {
            this.modal.mode = 'update'
            let index = this.data.findIndex(x => x == row)
            this.model.Id = this.data[index].Id
            this.model.Title = this.data[index].Title
            this.openModal()
        },
        deleteRow(row) {

            let params = {
                params: { ids: row },
                paramsSerializer: function(params) {
                    return qs.stringify(params, { arrayFormat: 'repeat' })
                },
            }


            if (!Array.isArray(row))
                params = {
                    params: { id: row.Id }
                }
            this.$http.delete('api/AccessType', params)
                .then(() => {
                    this.reloadTable()
                }).catch((error) => {
                    window.console.error(error);
                });
        }
    }
}
</script>

<style>

</style>
