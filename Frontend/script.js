
    const botao = document.getElementById('btnMenu');
    const menu = document.getElementById('Menu');

    botao.addEventListener('click', ()=>{
        menu.classList.toggle("visivel")}
    );

    const box = document.getElementById("Box");
// Modulos***************************************************************************************
    let modoRemocao = false;
    let modoEdicao = false;
//***********************************************************************************************
    async function carregarTarefas() {
        try {
            const resposta = await fetch("http://localhost:5006/api/tarefas");
            const tarefas = await resposta.json();

            box.innerHTML = ""; // Limpa o conteúdo

            tarefas.forEach(tarefa => {
                const div = document.createElement("div");
                div.className = "tarefa";
                let conteudo =  `
                    <p><strong>${tarefa.nome}</strong></p>
                    <p>Status: ${tarefa.concluida ? "✅ Concluída" : "🕓 Pendente"}</p>
                    <p>Feito <input type="checkbox" ${tarefa.concluida ? "checked" : ""} data-id="${tarefa.id}"></p>
                `;
                if(modoRemocao){
                    conteudo += `<button class="btn-remover-tarefa" data-id="${tarefa.id}">❌ Remover</button>`;
                }

// Se o modoRemocao estiver ativo, adicionar os listeners de remoção
                if(modoRemocao){
                    const botoesRemover = document.getElementsByClassName("btn-remover-tarefa");

                    Array.from(botoesRemover).forEach(botao => {
                        botao.addEventListener("click", async () => {
                            const id = botao.getAttribute("data-id");
                            const confirmar = confirm("Deseja realmente remover esta tarefa?");
                            if (!confirmar) return;

                            try {
                                await fetch(`http://localhost:5006/api/tarefas/${id}`, {
                                    method: "DELETE"
                                });
                                carregarTarefas();
                            } catch (erro) {
                                console.error("Erro ao remover tarefa:", erro);
                            }
                        });
                    });
                }
// Se o modoEdição estiver ativo, adicionar os listeners de edição
                if(modoEdicao){
                    conteudo += `<button class="btn-editar-tarefa" data-id="${tarefa.id}">Editar</button>`;
                }
                conteudo += `<hr>`;

                div.innerHTML = conteudo;
                box.appendChild(div);
            });

// Depois de carregar os elementos
            document.querySelectorAll(".tarefa input[type='checkbox']").forEach(checkbox => {checkbox.addEventListener("change", async () => {
                const id = checkbox.getAttribute("data-id");
                const nome = checkbox.closest(".tarefa").querySelector("strong").innerText;
                const concluida = checkbox.checked;

                const tarefaAtualizada = {
                    id: parseInt(id),
                    nome: nome,
                    concluida: concluida
                };

                try {
                    await fetch(`http://localhost:5006/api/tarefas/${id}`, {
                        method: "PUT",
                        headers: {
                            "Content-Type": "application/json"
                        },
                        body: JSON.stringify(tarefaAtualizada)
                    });
                    carregarTarefas();
                    console.log("Tarefa atualizada com sucesso.");
                } catch (erro) {
                    console.error("Erro ao atualizar tarefa:", erro);
                }
                });

            });


        } catch (erro) {
            console.error("Erro ao carregar tarefas:", erro);
            box.innerHTML = "<p>Erro ao carregar tarefas</p>";
        }
    }

    // Quando a página carrega
    document.addEventListener("DOMContentLoaded", carregarTarefas);

    //formulário para adicionar tarefas
    const btnAdicionar = document.getElementById("btnAdicionar");
    const modal = document.getElementById("formAdicionar");
    const inputNome = document.getElementById("inputNomeTarefa");
    const btnConfirmar = document.getElementById("btnConfirmarAdicionar");
    const btnCancelar = document.getElementById("btnCancelarAdicionar");

    btnAdicionar.addEventListener("click", () => {
        modal.classList.remove("oculto");
        inputNome.value = "";
        inputNome.focus();
    });

    btnCancelar.addEventListener("click", () => {
        modal.classList.add("oculto");
    });

    btnConfirmar.addEventListener("click", async () => {
        const nome = inputNome.value.trim();
        if (nome === "") {
            alert("Digite o nome da tarefa.");
            return;
        }

        const novaTarefa = {
            nome: nome,
            concluida: false
        };

        try {
            await fetch("http://localhost:5006/api/tarefas", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(novaTarefa)
            });

            modal.classList.add("oculto");
            carregarTarefas(); // Atualiza a lista de tarefas
        } catch (erro) {
            console.error("Erro ao adicionar tarefa:", erro);
        }
    });
// Remover tarefa ********************************************************************************
    //botoes
    const btnRemover = document.getElementById("btnRemover");
    const btnConfirmarRemocao = document.getElementById("btnConfirmarRemocao");
    const btnCancelarRemocao = document.getElementById("btnCancelarRemocao");
    const modulo = document.getElementById("formRemover")//adicinar formulario de confirmação de remoção 

    //funções
    btnRemover.addEventListener("click",()=>{
        menu.classList.toggle("visivel");
        modoRemocao = !modoRemocao;
        carregarTarefas();
    });    
//Editar tarefas *********************************************************************************
    const btnEditar = document.getElementById("btnEditar");

    btnEditar.addEventListener("click", ()=>{
        menu.classList.toggle("visivel");
        modoEdicao = !modoEdicao;
        carregarTarefas();
    })

    

    
