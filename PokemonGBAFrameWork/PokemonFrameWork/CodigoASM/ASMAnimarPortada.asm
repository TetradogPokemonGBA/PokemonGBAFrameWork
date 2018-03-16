.thumb
Start:
        push {r0-r3}
        ldr r3, .RAM_Address
        ldr r1, .Ani_Table
        ldrb r2, [r3, #0x1]
        lsl r2, #0x3
        add r1, r2
        ldrb r0, [r1, #0x4]
        ldrb r2, [r3]
        cmp r2, r0
        bge LoadFrame
        add r2, #0x1
        strb r2, [r3]
        b Finish
LoadFrame:
        mov r2, #0x0
        strb r2, [r3]
        ldrb r2, [r1, #0x5]
        strb r2, [r3, #0x1]
        ldr r0, [r1]
        ldr r1, .VRAM_Animated_Image
        swi #0x12

Finish:
        pop {r0-r3}
ExitRightNow:
        ldr r1, [r0]
        ldr r0, .Frame_Count
        push {r3}
        ldr r3, .Old_Routine
        bx r3

.align 2
.Ani_Table:
        .word 0x08FFFFFF
.RAM_Address:
        .word 0x020370c0
.VRAM_Animated_Image:
        .word 0x06004000
.Frame_Count:
        .word 0x0000FFFF
.Old_Routine:
        .word 0x08078C01