import Sidebar from "./Sidebar";

const Main = ({children}) => {
    return (
        <main class="flex flex-row basis-11/12 bg-gray-50">
            <Sidebar />
            {children}
        </main>
    );
};

export default Main;