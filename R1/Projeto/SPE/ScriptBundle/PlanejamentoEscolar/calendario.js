$(document).ready(function () {

	var date = new Date();
	var d = date.getDate();
	var m = date.getMonth();
	var y = date.getFullYear();

	$('#calendario').fullCalendar({
		header: {
			left: 'prev,next', //, today',
			center: 'title',
			right: 'month,basicWeek,basicDay'
		},
		events: function(start, end, callback) {
			$.ajax({
				url: '/Calendario/AgendaAmbienteDocente',
				dataType: 'json',
				data: {
					start: Math.round(start.getTime() / 1000),
					end: Math.round(end.getTime() / 1000),
					turmaId: $("#turmaId").val()
				},
				success: function(events) {
				    callback(events);                  
				    
				}
			});

		},

		monthNames: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
		monthNamesShort: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
		dayNames: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sábado'],
		dayNamesShort: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sábado'],
		buttonText:{
		    month: "mês",
		    week: "semana",
            day: "dia"
		},
		//editable: true,
		viewDisplay: function(view){

			$(".fc-day").each(function() {
				$(this).addClass("day-"+$(this).find(".fc-day-number").html());
			});

			dateArr = [];                    
			var today = $('#calendario').fullCalendar('getDate');
			var viewData = $('#calendario').fullCalendar('getView');
			cMonth = today.getMonth();
			cYear = today.getFullYear();
			$('.fc-day-number').each(function(){
					
			lDay = parseInt($(this).text());
			lYear = parseInt(cYear);
			//check if it is another month date
			if($(this).parents('td').hasClass('fc-other-month'))
			{
				
				//if it is belong to the previous month
				/*if(lDay>15)
				{
					lMonth = parseInt(cMonth) - 1;
					lDate = new Date(lYear,lMonth,lDay);
					dateArr.push(lDate);
				}
				else //belong to the next month
				{
					lMonth = parseInt(cMonth) + 1;
					lDate = new Date(lYear,lMonth,lDay);
					dateArr.push(lDate);
				}*/
			}
			else
			{
				lMonth = parseInt(cMonth);
				lDate = new Date(lYear,lMonth,lDay);
				dateArr.push(lDate);
			}
					
		});
			},
		eventRender: function (event, element) {
		    $(".fc-event").each(function () {
		        $(".fc-event").attr("data-toggle", 'tooltip');
		        $(".fc-event").attr("data-placement", 'right');
		        $(".fc-event").attr("data-original-title", event.title + '<br>' + event.description + '<br><b>CH:</b>' + event.carga + '<br><b>Matriz:</b>' + event.matriz + '<br><b>Turno:</b>' + event.turno + '<br><b>Data Início Turma:</b><br>' + event.dataInicioTurma + '<br><b>Data Fim Turma:</b>' + event.dataFinalTurma + '<br><b>Data Início Componente:</b><br>' + event.originalStart + '<br><b>Data Fim Componente:</b><br>' + event.originalEnd);
		        $(".fc-event").attr("alt", 'tootip');
		        //data-toggle='tooltip' data-placement='top' title='' data-original-title='Calendário' alt='tootip'
		    })
		    $("div[alt='tootip']").popover({ trigger: "hover", html:true });
			element.html(
				"<span class='eventTitle'>" + event.title + "</span>" +
				"<br />"+
				"<span class='eventDescription'>"+event.description+"</span>");

			for(var i in dateArr) {
				if(dateArr[i].getTime() == event.start.getTime())
				{
					var date = new Date(dateArr[i].getTime());
					$('td.day-'+date.getDate()).not(".fc-other-month").addClass("dayWithEvent");
				}
			}
		}
		//eventMouseover: function (event, element) {

		//    //alert('Coordinates: ' + event.title + ',' + event.description);
		//}
	});



});