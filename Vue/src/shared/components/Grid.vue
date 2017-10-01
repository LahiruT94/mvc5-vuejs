<template>
  <div>
    <el-input type="query" v-model="filter" placeholder="Search..."></el-input>

    <el-table ref="multipleTable" :data="filteredData" emptyText="Пусто" border style="width: 100%" @selection-change="handleSelectionChange">
      <el-table-column type="expand">
        <template scope="scope">
          <p>name: {{ scope.row.name }}</p>
          <p>power: {{ scope.row.power }}</p>
        </template>
      </el-table-column>

      <el-table-column type="selection" width="55"></el-table-column>

      <el-table-column property="id" sortable label="id" width="120"></el-table-column>

      <el-table-column property="name" sortable label="Имя" width="120"></el-table-column>

      <el-table-column property="power" sortable label="Сила" show-overflow-tooltip></el-table-column>

      <el-table-column label="Действия">
        <template scope="scope">
          <el-button size="small" @click="handleEdit(scope.row)">Редактировать</el-button>
          <el-button size="small" type="danger" @click="handleDelete(scope.row)">Удалить</el-button>
        </template>
      </el-table-column>

    </el-table>

    <div style="margin-top: 10px">
      <el-button v-show="hasSelectedElements" @click="clearSelected()">Удалить выбранные</el-button>
      <el-button v-show="hasElements" @click="clearGrid()">Очистить список</el-button>
      <el-button @click="handleCreate()">Добавить бойца</el-button>
    </div>

    <el-dialog title="Добавить бойца" :visible.sync="modal.show">
      <el-form :model="model">
        <el-form-item label="Имя" :label-width="formLabelWidth">
          <el-input v-model="model.name" placeholder="Имя"></el-input>
        </el-form-item>
        <el-form-item label="Сила" :label-width="formLabelWidth">
          <el-input v-model="model.power" placeholder="Сила"></el-input>
        </el-form-item>
      </el-form>
      <span slot="footer" class="dialog-footer">
        <el-button @click="closeModal()">Cancel</el-button>
        <el-button type="primary" @click="confirmModal()">Confirm</el-button>
      </span>
    </el-dialog>
  </div>
</template>


<script>

export default {
  name: 'grid',
  props: {
    data: Array
  },
  data() {
    let sortOrders = {}
    Object.keys(this.data).forEach((key) => {
      sortOrders[key] = 1
    })
    return {
      sortKey: '',
      sortOrders: sortOrders,
      gridData: {
        data: this.data,
        selected: [],
      },
      filter: this.filterKey,
      model: {},
      formLabelWidth: '50px',
      modal: {
        show: false,
        size: 'modal-sm',
        id: 'exampleModal'
      }
    }
  },
  computed: {
    filteredData() {
      let sortKey = this.sortKey
      let filterKey = this.filter && this.filter.toLowerCase()
      let order = this.sortOrders[sortKey] || 1
      let data = this.gridData.data
      if (filterKey) {
        data = data.filter((row) => {
          return Object.keys(row).some((key) => {
            return String(row[key]).toLowerCase().indexOf(filterKey) > -1
          })
        })
      }
      if (sortKey) {
        data = data.slice().sort((a, b) => {
          a = a[sortKey]
          b = b[sortKey]
          return (a === b ? 0 : a > b ? 1 : -1) * order
        })
      }
      return data
    },
    hasElements() {
      return this.gridData.data.length > 0
    },
    hasSelectedElements() {
      return this.gridData.selected.length > 0
    }
  },
  methods: {
    validateModel(model) {
      if (model && model.name !== undefined && model.power !== undefined)
        return true
      else
        return false

    },
    sortBy(key) {
      this.sortKey = key
      this.sortOrders[key] = this.sortOrders[key] * -1
    },
    confirmModal() {
      if (this.validateModel(this.model)) {
        
        let index = this.gridData.data.findIndex(x => x == this.model)
        if (index !== -1) {
          this.gridData.data[index].name = this.model.name
          this.gridData.data[index].power = this.model.power
        } else {
          this.gridData.data.push(this.model)
        }

        this.model = {
          id: this.getNewId
        }
        this.closeModal()
      }
    },
    deleteRow(row) {
      let index = this.gridData.data.indexOf(row)
      if (index !== -1) {
        this.gridData.data.splice(index, 1)
      }
    },
    openModal() {
      this.modal.show = true
    },
    closeModal() {
      this.modal.show = false
    },
    clearSelected() {
      this.gridData.selected.forEach((row) => {
        this.deleteRow(row)
      })
    },
    clearGrid() {
      this.gridData.data = []
    },
    handleSelectionChange(val) {
      this.gridData.selected = val
    },
    handleCreate() {
      this.model = {
        id: this.getNewId
      }
      this.openModal()
    },
    handleEdit(row) {

      let index = this.gridData.data.findIndex(x => x == row)
      this.model.id = this.gridData.data[index].id
      this.model.name = this.gridData.data[index].name
      this.model.power = this.gridData.data[index].power
      this.openModal()
    },
    handleDelete(row) {
      this.deleteRow(row)
    }
  }
}
</script>

<style>

</style>
