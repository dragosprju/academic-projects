.MODEL Tiny
.DATA
    my_arr db 9,4
.CODE

mov bx, offset my_arr

;swapuim - modul de apelare la http://www.cs.rochester.edu/courses/252/spring2014/notes/03a_procedures.pdf
push [bx+1]
push [bx]
call swap

;imprimam nr1
mov dx, [bx]
add dx, '0'
mov ah, 02h
int 21h

;imprimam spatiu
mov dx, ' '
mov ah, 02h
int 21h

;imprimam nr2
mov dx, [bx+1]
add dx, '0'
mov ah, 02h
int 21h

;iesim
mov ah, 4Ch
int 21h

swap:
    push bp
    mov bp, sp
    push bx
    
    mov cx,[bp+6]
    and cx, 0FFh
    mov dx,[bp+4]
    and dx, 0FFh
    mov ax,cx
    mov bx,dx
    mov dx,ax
    mov cx,bx
    
    mov bx,[bp-2]
    mov [bx], dx
    mov [bx+1], cx
    mov sp,bp
    pop bp
ret