<template>
    <div class="access-type-create">
        <access-type-form @submit="submit" :model="model" :mode="mode" />
    </div>
</template>

<script>
import form from '@admin/AccessType/_form.vue'
import { EventBus } from './utilities/event-bus';

export default {
    props: ['model'],
    components: { 'access-type-form': form },
    data() {
        return {
            mode: 'create'
        }
    },
    methods: {
        submit(model) {
            this.$http.post('api/AccessType', model)
                .then(() => {
                    EventBus.$emit('created');
                })
                .catch((error) => {
                    window.console.error(error);
                });
        }
    }

}
</script>

