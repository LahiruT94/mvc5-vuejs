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
            <el-button type="primary" @click="$emit('submit', mode)" v-text="text.Button"></el-button>
        </span>
    </el-dialog>
</template>

<script>

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
    }
}
</script>

