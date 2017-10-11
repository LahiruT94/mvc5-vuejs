<template>
        <div class="access-type-index">

            <h1>Типы доступа</h1>

            <div class="table-header-group">
                <el-input type="query" v-model="filter" @change="filterChanged" placeholder="Search..."></el-input>
                <el-button v-show="hasSelectedElements" @click="clearSelected()">Удалить выбранные</el-button>
                <el-button @click="createAccessType()">Добавить тип доступа</el-button>
            </div>

            <el-table :data="getAccessTypeList" emptyText="Пусто" border style="width: 100%" @selection-change="handleSelectionChange" @sort-change="handleSort" :default-sort="sortOrder">

                <el-table-column type="selection" width="55"></el-table-column>

                <el-table-column property="Id" sortable="custom" label="id" width="120"></el-table-column>

                <el-table-column property="Title" sortable="custom" label="Тип доступа" width="120"></el-table-column>

                <el-table-column label="Действия">
                    <template scope="scope">
                        <el-button size="small" @click="editAccessType(scope.row)">Редактировать</el-button>
                        <el-button size="small" type="danger" @click="deleteAccessType(scope.row)">Удалить</el-button>
                    </template>
                </el-table-column>

            </el-table>

            <el-pagination @size-change="handleSizeChange" @current-change="handleCurrentChange" :current-page="getCurrentPage" :page-sizes="[10, 20, 30, 40]" :page-size="getPageSize" layout="total, sizes, prev, pager, next, jumper" :total="getTotalItems" />
            <AccessTypeModalForm :mode="modal.mode" :model="model" :modal="modal" @cancel="cancel" @submit="submit" />
        </div>
</template>


<script>
import AccessTypeModalForm from '@admin/AccessType/_form.vue'
import { mapGetters } from 'vuex'
import qs from 'qs'

export default {
    name: 'access-type-index',
    components: {
        AccessTypeModalForm
    },
    beforeCreate() {
        this.$store.dispatch('getAccessType')
    },
    data() {
        return {
            selected: [],
            filter: this.filterKey,
            sortOrder: {},
            delayTimer: 0,
            model: { Title: "" },
            formLabelWidth: '50px',
            modal: {
                show: false,
                mode: 'create'
            }
        }
    },
    computed: {
        ...mapGetters(['getAccessTypeList', 'getTotalItems', 'getCurrentPage', 'getPageSize']),
        hasSelectedElements() {
            if (this.selected)
                return this.selected.length > 0
            return false
        }
    },
    methods: {
        reload() {
            this.$store.dispatch('getAccessType')
        },
        handleSizeChange(value) {
            this.$store.dispatch('setPageSize', value)
            this.reload()
        },
        handleCurrentChange(value) {
            this.$store.dispatch('setPage', value)
            this.reload()
        },
        handleSort({ prop, order }) {
            this.$store.dispatch('setOrder', { prop, order })
            this.reload()
        },
        filterChanged(value) {
            clearTimeout(this.delayTimer)
            this.delayTimer = setTimeout(() => {
                this.$store.dispatch('setFilter', value)
                this.reload()
            }, 1000);
        },
        cancel() {
            this.closeModal()
        },
        submit(mode) {
            this.$store.dispatch(mode + 'AccessType', this.model)
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
        deleteAccessType(row) {
            if (Array.isArray(row)) {
                this.$store.dispatch('deleteMultipleAccessTypes', {
                    params: { ids: row },
                    paramsSerializer: function(params) {
                        return qs.stringify(params, { arrayFormat: 'repeat' })
                    }
                })
            } else {
                this.$store.dispatch('deleteAccessType', {
                    params: { id: row.Id }
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
        margin: 3px 
</style>
