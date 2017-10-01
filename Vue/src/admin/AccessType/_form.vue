<template>
    <div class="access-type-form">
        <form>
            <div class="form-group">
                <label for="Title">Title</label>
                <input v-model.trim="model.Title" type="text" class="form-control" id="Title">
            </div>
        </form>
    </div>
</template>

<script>
import { EventBus } from './utilities/event-bus';

export default {
    props: ['model', 'mode'],
    created() {
        EventBus.$on('submitted', () => {
            this.submit()
        })
    },
    methods: {
        validateModel() {
            if (this.model && this.model.Title !== undefined && this.model.Title !== "")
                return true
            else
                return false
        },
        submit() {
            if (this.validateModel())
                this.$emit("submit", this.model)
            else
                window.console.log('fail validate')
        }
    }
}
</script>

