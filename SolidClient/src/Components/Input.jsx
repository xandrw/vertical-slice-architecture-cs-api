const Input = ({name, type, valueSignal, valueSetter, errorsSignal}) => {
    return (
        <label class="block mb-4">
            {name.charAt(0).toUpperCase() + name.slice(1)}
            <input name={name} type={type} value={valueSignal()}
                   class="form-input"
                   classList={{invalid: errorsSignal()[name].length > 0}}
                   on:input={e => valueSetter(e.target.value)}
            />
            {errorsSignal()[name] && <span class="text-xs text-red-500">{errorsSignal()[name]}</span>}
        </label>
    );
};

export default Input;