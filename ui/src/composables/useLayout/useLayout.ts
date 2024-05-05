import { markRaw, shallowRef } from 'vue'
import { LayoutComponent } from './types'
import DefaultLayout from '../../layouts/default.vue'
import AdminLayout from '../../layouts/admin.vue'

const layoutComponent = shallowRef<{ component: any }>()

export function useLayout() {
    
    const setLayout = (type: LayoutComponent) => {
        switch (type) {
            case 'admin':
                return layoutComponent.value = {
                    component: markRaw(AdminLayout)
                }
            default:
                return layoutComponent.value = {
                    component: markRaw(DefaultLayout)
                }
        }
    }

    return {
        layoutComponent,
        setLayout
    }
}
