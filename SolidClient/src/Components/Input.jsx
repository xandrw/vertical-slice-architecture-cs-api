const Input = props => (
    <label class="block mb-4">
        {props.name.charAt(0).toUpperCase() + props.name.slice(1)}
        <input name={props.name} type={props.type} value={props.value}
               class="form-input"
               classList={{invalid: props.errors[props.name].length > 0}}
               on:input={e => props.setter(e.target.value)}
        />
        {props.errors[props.name] && <span class="text-xs text-red-500">{props.errors[props.name]}</span>}
    </label>
);

export default Input;