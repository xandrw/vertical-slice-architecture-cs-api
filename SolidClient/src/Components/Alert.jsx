const Alert = ({errorsSignal, errorKey}) => {
    return (
        <>{errorsSignal()[errorKey] &&
            <div class="text-red-500 text-sm bg-red-50 mb-4 border border-red-500 rounded-lg p-2">
                {errorsSignal()[errorKey]}
            </div>}
        </>
    );
};

export default Alert;