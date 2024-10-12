import Header from "./Header";
import Main from "./Main";

const DefaultLayout = () => {
    return (
        <div class="flex flex-col h-screen bg-gray-50">
            <Header />
            <Main></Main>
        </div>
    );
};

export default DefaultLayout;