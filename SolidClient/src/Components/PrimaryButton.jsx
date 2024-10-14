const PrimaryButton = ({type = "button", fullWidth = false, children}) => (
    <button type={type}
            classList={{'w-full': fullWidth}}
            class="px-4 py-2 text-center text-white bg-purple-600 rounded-lg active:bg-purple-600 hover:bg-purple-700 focus:outline-offset-0 focus:outline-4 focus:outline-purple-100 focus:border-purple-400 outline-none">
        {children}
    </button>
);

export default PrimaryButton;