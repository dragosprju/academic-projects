    mov cx, 5
    mov ax, 1
bucla:
    mul cl
    dec cx
    cmp cx, 0
    jnz bucla
    mov dx, ax
    mov cx, 0
bucla2:
    inc cx
    mov bl,0Ah
    div bl
    mov bl,ah
    add bx,'0'
    push bx
    and ax,00FFh
    cmp ax,0
    jnz bucla2
bucla3:
    dec cx
    pop dx
    mov ah,02h
    int 21h
    cmp cx,0
    jnz bucla3
mov ah, 4Ch
int 21h
