################################################################################
# Automatically-generated file. Do not edit!
################################################################################

# Add inputs and outputs from these tool invocations to the build variables 
C_SRCS += \
../src/queue_hw_0/queue_hw_0.c 

OBJS += \
./src/queue_hw_0/queue_hw_0.o 

C_DEPS += \
./src/queue_hw_0/queue_hw_0.d 


# Each subdirectory must supply rules for building sources it contributes
src/queue_hw_0/%.o: ../src/queue_hw_0/%.c
	@echo 'Building file: $<'
	@echo 'Invoking: MicroBlaze gcc compiler'
	mb-gcc -Wall -O0 -g3 -c -fmessage-length=0 -MT"$@" -I../../mb0_bsp/microblaze_0/include -mlittle-endian -mcpu=v9.6 -mxl-soft-mul -Wl,--no-relax -MMD -MP -MF"$(@:%.o=%.d)" -MT"$(@)" -o "$@" "$<"
	@echo 'Finished building: $<'
	@echo ' '


