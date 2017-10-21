<template>
    <el-dialog v-bind:title="text.Title" :visible.sync="modal.show">
        <form class="access-type-form">
            <div class="form-group">
                <label>Title
                    <input v-model.trim="model.Title" class="form-control" id="Title">
                </label>
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
                    const valid = ['create', 'update']
                    return valid.indexOf(value.toLowerCase()) >= 0
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

