<template>
    <el-dialog v-bind:title="text.Title" :visible.sync="modal.show">
        <form class="access-type-form">
            <div class="form-group">
                <label for="Title">Title</label>
                <input v-model.trim="model.Title" type="text" class="form-control" id="Title">
            </div>
        </form>
        <span slot="footer" class="dialog-footer">
            <el-button @click="$emit('cancel')">Отмена</el-button>
            <el-button type="primary" @click="$emit('submit')">{{text.Button}}</el-button>
        </span>
    </el-dialog>
</template>

<script>
import AccessTypeForm from '@admin/AccessType/_form.vue'

export default {
    props: {
        model: {
            type: Object,
            required: true
        },
        modal: {
            type: Object,
            required: true
        },
        mode: {
            type: String,
            required: true,
            default: 'create'
        },
    },
    computed: {
        text() {
            let create = {
                Title: 'Добавить тип доступа',
                Button: 'Добавить'
            }
            let update = {
                Title: 'Редактировать тип доступа',
                Button: 'Сохранить'
            }

            if (this.mode === 'update')
                return update
            else
                return create
        }
    },
    components: { 'access-type-form': AccessTypeForm }
}
</script>

