namespace Features.PInput.Entities
{
    public class InputCacheE
    {
        public string field;
        public float value;

        public bool oncePressable;
        public bool wasPressed;

        public InputCacheE(string field, float value, bool oncePressable = false)
        {
            this.field = field;
            this.value = value;
            this.oncePressable = oncePressable;
            wasPressed = false;
        }

        public void SetValue(float value)
        {
            if (oncePressable)
            {
                if (!wasPressed && value > 0.5f)
                {
                    wasPressed = true;
                }
                else if (wasPressed && value <= 0.5f)
                {
                    this.value = 0;
                    wasPressed = false;
                    return;
                }
                else if (wasPressed)
                {
                    this.value = 0;
                    return;
                }
            }

            this.value = value;
        }
        public float GetValue()
        {
            return value;
        }
    }
}
