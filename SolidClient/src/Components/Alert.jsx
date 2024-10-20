const Alert = ({errors, errorKey}) => {
    return (
        <>{errors[errorKey] &&
            <div class="text-red-500 text-sm bg-red-50 mb-4 border border-red-500 rounded-lg p-2">
                {errors[errorKey]}
            </div>}
        </>
    );
};

export default Alert;