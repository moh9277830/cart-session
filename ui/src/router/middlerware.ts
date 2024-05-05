import { NavigationGuardNext, RouteLocation } from "vue-router";
import { useLayout } from "../composables/useLayout";
import { LayoutComponent } from "../composables/useLayout";

export const layoutMiddleware = async (to: RouteLocation, _from: RouteLocation, next: NavigationGuardNext) => {
    const layout = to.matched.find(item => item.meta.layout) ? to.meta.layout as LayoutComponent : 'default';
    
    const { setLayout } = useLayout()
    
    setLayout(layout);
    
    next();
}