#pragma config OSC = HS
#pragma config WDT = OFF
#pragma config LVP = OFF

#include <p18f4520.h>
#include <usart.h>
#include <adc.h>
#include <stdio.h>
#include <timers.h>


int periodOneSecond = 0, timerCount;


void configureUSART(void)
{
    OpenUSART(USART_TX_INT_OFF & USART_RX_INT_OFF & USART_ASYNCH_MODE & USART_EIGHT_BIT & USART_CONT_RX & USART_BRGH_HIGH, 51);
}

void configureADC(void)
{
    OpenADC(ADC_FOSC_8 & ADC_RIGHT_JUST & ADC_20_TAD, ADC_CH0 & ADC_INT_OFF & ADC_VREFPLUS_EXT & ADC_VREFMINUS_VSS, 0b1010);
}

void configureAnalogInputPins(void)
{
    TRISAbits.TRISA0 = 1;
    TRISAbits.TRISA1 = 1;
    TRISAbits.TRISA4 = 1;
}

void configureTimer0(void)
{
    CloseTimer0 ();
    OpenTimer0 (TIMER_INT_ON & T0_16BIT & T0_SOURCE_INT & T0_PS_1_1);
    WriteTimer0(0); // Load initial value for 1 ms period with 8 MHz oscillator and 1:8 prescaler
    INTCONbits.GIE = 1;          // Enable global interrupts  
    INTCONbits.TMR0IE = 1;       // Enable Timer 0 Interrupt  
    INTCONbits.TMR0IF = 0;
}


void TMR0_ISR(void) 
{  
      INTCONbits.TMR0IE = 0;    // Disable any interrupt within interrupts  
      timerCount++;
      if(timerCount > 29){ periodOneSecond = 1 ; timerCount = 0; }
      WriteTimer0 (0);    	// Clear Timer 
      INTCONbits.TMR0IE = 1;    // Re-enable TMR0 interrupts
      INTCONbits.TMR0IF = 0;    // Clear TMR0 flag before returning to main program
} 
   
void sendData(int sensor1, int sensor2, int sensor3)
{
    char str[50];
    sprintf(str, "{\"AN0\": %d, \"AN1\": %d, \"AN4\": %d}\r\n", sensor1, sensor2, sensor3);
    putsUSART(str);
}

int readSensor(unsigned int channel)
{
    float result;
    SetChanADC(channel);
    ConvertADC();
    while (BusyADC());
    result = ReadADC();

    
    if (channel == ADC_CH0)
    {
        return (int)((result / 5.115) * 10 / 10);
    }
    else if (channel == ADC_CH1)
    {
        return (int)(result * 1000 / 1024);
    }
    else if (channel == ADC_CH4)
    {
        return (int)(((result * (5.0 / 1023.0) - 0.2) * (250.0 / 4.5)) * 10);
    }
    return 0;
}


void main(void)
{
    int sensor1, sensor2, sensor3;
    
    configureUSART();
    configureADC();
    configureAnalogInputPins();
    configureTimer0();

    timerCount = 0;

    while (1)
    {
        if (periodOneSecond == 1)
        {
            // Reset the periodOneSecond flag
            periodOneSecond = 0;

            // Read sensor values
            sensor1 = readSensor(ADC_CH0);
            sensor2 = readSensor(ADC_CH1);
            sensor3 = readSensor(ADC_CH4);

            // Send sensor values over USART
            sendData(sensor1, sensor2, sensor3);
        }
    }
}


#pragma interrupt high_isr
void high_isr(void)
{
    if (INTCONbits.TMR0IF)
    {
        TMR0_ISR();
    }
}


#pragma code high_vector = 0x08
void high_vector(void)
{
    _asm goto high_isr _endasm
}
