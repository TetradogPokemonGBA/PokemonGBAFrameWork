.align 2
.thumb

push {r0-r3,lr}
ldr r0, =0x02024284
ldr r1, =0x020370C0
ldrb r1, [r1]
mov r2, #0x64
mul r1, r2
add r0, r0, r1
ldr r3, =(0x803E47C +1)
bl linker
po {r0-r3,pc}

linker:
bx r3