/*
Note: both image and palette should be LZ77 compressed!
Table format: [image pointer][pal pointer]..........
*/
.equiv table_location, 0x8900000
.thumb
push {r4-r5, lr}
sub sp, sp, #0x18
ldr r0, =0x20370C0
ldrh r0, [r0, #2]
mov r1, #0
mov r2, #0x14
mul r2, r0
mov r3, #4
mov r0, #10
str r0, [sp]
str r0, [sp, #4]
mov r0, #0xD
str r0, [sp, #8]
mov r0, #0x40
str r0, [sp, #0xC]
add r0, sp, #0x10
ldr r4, =0x810FE51
bl bx_r4
add r0, sp, #0x10
ldr r4, =0x8003CE5
bl bx_r4
ldr r4, =0x2039990
strb r0, [r4]
ldr r4, =0x8003FA1
bl bx_r4
ldr r0, =0x20370C0
ldrh r0, [r0]
ldr r1, =table_location
lsl r0, r0, #3
add r0, r0, r1
ldr r5, [r0, #4]
ldr r0, [r0]
ldr r1, =0x6008800
swi 0x12
mov r0, r5
mov r1, #0xd0
mov r2, #0x20
ldr r4, =0x80703A9
bl bx_r4
mov r0, #0
ldr r4, =0x80020BD
bl bx_r4
add sp, sp, #0x18
pop {r4-r5, pc}

bx_r4: bx r4