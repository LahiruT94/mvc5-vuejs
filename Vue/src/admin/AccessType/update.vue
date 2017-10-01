<template>
    <div class="access-type-update">
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
            mode: 'update'
        }
    },
    methods: {
        submit(model) {
            this.$http.put('api/AccessType', model)
                .then(() => {
                    EventBus.$emit('updated');
                })
                .catch((error) => {
                    window.console.error(error);
                });
        }
    }

}
</script>

