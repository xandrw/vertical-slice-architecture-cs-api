import {A, useLocation} from "@solidjs/router";

const SidebarLink = ({route, name, isChild = false, children}) => {
    const location = useLocation();
    
    return (<>
        <A href={route}
           class="relative flex h-11 items-center w-full pl-6 text-sm font-semibold text-gray-800 border-l-4"
           classList={{'bg-purple-50 border-purple-50': isChild}}
           activeClass="bg-purple-100 border-purple-600"
           inactiveClass="border-white hover:bg-purple-50 hover:border-purple-400"
        >
            {name}
        </A>
        {location.pathname.includes(route) ? children : null}
    </>);
};

export default SidebarLink;