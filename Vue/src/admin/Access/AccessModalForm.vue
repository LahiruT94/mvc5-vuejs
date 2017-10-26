<template>
    <el-dialog v-bind:title="text.Title" :visible.sync="modal.show">
        <form class="access-form">
            <div class="form-group">
                <label>Адрес
                    <input v-model.trim="model.Address" class="form-control">
                </label>
                <label>Логин
                    <input v-model.trim="model.Login" class="form-control">
                </label>
                <label>Пароль
                    <input v-model.trim="model.Password" class="form-control">
                </label>
                <label>Заметка
                    <input v-model.trim="model.Note" class="form-control">
                </label>
                <label>Тип доступа
                    <input v-model.trim="model.AccessType" class="form-control">
                </label>
                <label>Проект
                    <input v-model.trim="model.Project" class="form-control">
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
                    Title: 'Добавить доступ',
                    Button: 'Добавить'
                }
                let update = {
                    Title: 'Редактировать доступ',
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

